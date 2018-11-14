module App.State

open App.Types

let init _ =
    {
        CurrentAttempt = Color.Red, Color.Red, Color.Red, Color.Red
        Attempts =  List.empty
    }

let update msg model =
    model
