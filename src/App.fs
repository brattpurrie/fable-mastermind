module App.View

open Elmish
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fulma
open Fulma.FontAwesome
open App.Types
open App.State

let parseToColor (color:obj) =
    match color with
    | :? Color as c -> c
    | _ -> Red

let tupleToList (t:System.Tuple<Color,Color,Color,Color>) = 
    if Microsoft.FSharp.Reflection.FSharpType.IsTuple(t.GetType()) 
        then Some (Microsoft.FSharp.Reflection.FSharpValue.GetTupleFields t |> Array.toList |> List.map parseToColor)
        else None
        
let colorToCssClass color =
    match color with
    | Color.Red -> "red"
    | Color.Green -> "green"
    | Color.Blue -> "blue"
    | Color.Yellow -> "yellow"
    | Color.Orange -> "orange"
    | Color.Violet -> "violet"
        
// type Dispatch<'Msg> = Msg -> unit
        
let renderColor dispatch idx color =
    div [ ClassName "color pointer"
          Key (sprintf "input_%d" idx)
          OnClick (fun _ -> Msg.ChangeColor idx |> dispatch) ] [
        Icon.faIcon [Icon.CustomClass (colorToCssClass color)] [ Fa.icon Fa.I.Circle ; Fa.fa3x ]    
    ]
    

let currentAttemptForm model dispatch =
    let colorInput =
        tupleToList model.CurrentAttempt
        |> Option.map (fun colors ->
            colors
            |> List.mapi (fun idx color ->
                renderColor dispatch idx color
            )
            |> ofList
        )
        |> ofOption
    
    form [OnSubmit (fun ev -> ev.preventDefault())] [
        colorInput
        Button.button [ Button.Color IsPrimary
                        Button.OnClick (fun _ -> dispatch SubmitAttempt) ] [str "Submit"]
    ]

let gameOverScreen dispatch =
    Hero.hero [Hero.IsFullHeight; Hero.Color IsPrimary] [
       Hero.body [] [
           Container.container [ Container.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ] [
               Heading.h1 [Heading.Is1] [str "Game over"]
               Button.button [ Button.Color IsBlack
                               Button.OnClick (fun _ -> dispatch ResetGame) ] [str "Reset game"]
           ]
       ]
    ]

let private view model dispatch =
    // div (*function*) List.empty (*collection of attributes for div*) List.empty (*collection of childs inside div*)
    let isGameOver = List.length model.Attempts = MaxAttempts
    
    if isGameOver then
        gameOverScreen dispatch
    else
        Container.container [ Container.IsFluid ] [
            div [] [] // previous attempt
            currentAttemptForm model dispatch
        ]

open Elmish.React
open Elmish.Debug
open Elmish.HMR

Program.mkSimple init update view
|> Program.withReactUnoptimized "elmish-app"
#if DEBUG
// |> Program.withDebugger
|> Program.withConsoleTrace
#endif
|> Program.run
