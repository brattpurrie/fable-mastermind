module App.State

open App.Types

let init _ =
    {
        CurrentAttempt = Color.Red, Color.Red, Color.Red, Color.Red
        Attempts = List.empty
    }
    
let colors = [| Red ; Green ; Blue ; Yellow ; Orange ; Violet |]

let indexOfColor color =
    colors
    |> Array.findIndex (fun c -> c = color)
    
let nextColor color =
    let currentIndex = indexOfColor color
    let nextIndex = (currentIndex + 1) % Array.length colors
    colors.[nextIndex]

let update msg model =
    match msg with
    | ChangeColor idx ->
        let (one, two, three, four) = model.CurrentAttempt
        
        let currentAttempt =
            match idx with
            | 0 -> nextColor one, two, three, four
            | 1 -> one, nextColor two, three, four
            | 2 -> one, two, nextColor three, four
            | 3 -> one, two, three, nextColor four
            | _ -> model.CurrentAttempt
            
            
        { model with CurrentAttempt = currentAttempt }
          
    | SubmitAttempt ->
        { model with Attempts = model.CurrentAttempt::model.Attempts }
          
    | ResetGame -> init()
