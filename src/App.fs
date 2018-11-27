module App.View

open Elmish
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fulma
open Fulma.FontAwesome
open App.Types
open App.State
open App.Selector

let colorToCssClass color =
    match color with
    | Color.Red -> "red"
    | Color.Green -> "green"
    | Color.Blue -> "blue"
    | Color.Yellow -> "yellow"
    | Color.Orange -> "orange"
    | Color.Violet -> "violet"

// type Dispatch<'Msg> = Msg -> unit
let renderColor onclick idx color =
    div [ ClassName "color pointer"
          Key(sprintf "input_%d" idx)
          OnClick(fun _ -> onclick idx) ]
        [ Icon.faIcon [ Icon.CustomClass(colorToCssClass color) ] [ Fa.icon
                                                                        Fa.I.Circle
                                                                    Fa.fa3x ] ]

let add a b = a + b
let min a b = a - b

let currentAttemptForm model dispatch =
    let colorInput =
        tupleToList model.CurrentAttempt
        |> List.mapi (fun idx color -> renderColor (Msg.ChangeColor >> dispatch) idx color)
        |> ofList

    form [ OnSubmit(fun ev -> ev.preventDefault()) ]
        [ colorInput

          Button.button [ Button.Color IsPrimary
                          Button.OnClick(fun _ -> dispatch SubmitAttempt) ]
              [ str "Submit" ] ]

let gameOverScreen dispatch =
    Hero.hero [ Hero.IsFullHeight
                Hero.Color IsPrimary ]
        [ Hero.body []
              [ Container.container
                    [ Container.Modifiers
                          [ Modifier.TextAlignment
                                (Screen.All, TextAlignment.Centered) ] ]
                    [ Heading.h1 [ Heading.Is1 ] [ str "Game over" ]

                      Button.button
                          [ Button.Color IsBlack
                            Button.OnClick(fun _ -> dispatch ResetGame) ]
                          [ str "Reset game" ] ] ] ]

let renderGame model dispatch =
  let previousAttempts =
    model.Attempts
    |> List.rev
    |> List.mapi (fun idx attempt ->
      let key = sprintf "%i" idx
      let colorDivs =
        Selector.tupleToList attempt
        |> List.map (fun c -> renderColor ignore idx c)

      div [Key key] colorDivs
    )

  Container.container [ Container.IsFluid ]
      [ div [] previousAttempts
        currentAttemptForm model dispatch ]

let private view model dispatch =
    // div (*function*) List.empty (*collection of attributes for div*) List.empty (*collection of childs inside div*)
    let isGameOver = List.length model.Attempts = MaxAttempts
    if isGameOver then gameOverScreen dispatch
    else renderGame model dispatch


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
