module App.View

open Elmish
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fulma
open Fulma.FontAwesome

type Color = // enum
    | Red
    | Green
    | Blue
    | Yellow
    | Orange
    | Violet

type Attempt = Color * Color * Color * Color // Tuple

type Model =
    {
        CurrentAttempt : Attempt
        Attempts : Attempt list // list of Attempt
    }

type Msg =
    | ChangeValue of string

let init _ =
    {
        CurrentAttempt = Color.Red, Color.Red, Color.Red, Color.Red
        Attempts =  List.empty
    }

let private update msg model =
    model

let private view model dispatch =
    div (*function*) List.empty (*collection of attributes for div*) List.empty (*collection of childs inside div*)

open Elmish.React
open Elmish.Debug
open Elmish.HMR

// Program.mkSimple init update view
// |> Program.withReactUnoptimized "elmish-app"
// #if DEBUG
// |> Program.withDebugger
// #endif
// |> Program.run
