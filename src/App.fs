module App.View

open Elmish
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fulma
open Fulma.FontAwesome
open App.Types
open App.State


let private view model dispatch =
    // div (*function*) List.empty (*collection of attributes for div*) List.empty (*collection of childs inside div*)
    div [] [str "WIP 🙃"]

open Elmish.React
open Elmish.Debug
open Elmish.HMR

Program.mkSimple init update view
|> Program.withReactUnoptimized "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
