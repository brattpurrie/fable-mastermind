module App.Selector

open App.Types

let parseToColor (color : obj) =
    match color with
    | :? Color as c -> c
    | _ -> Red

let tupleToList (t : System.Tuple<Color, Color, Color, Color>) =
    if Microsoft.FSharp.Reflection.FSharpType.IsTuple(t.GetType()) then
        Microsoft.FSharp.Reflection.FSharpValue.GetTupleFields t
             |> Array.toList
             |> List.map parseToColor
    else List.empty

let calculatePins codeToCrack attempt =
    let codeList = tupleToList codeToCrack
    let attemptList = tupleToList attempt
    if List.length codeList <> List.length attemptList then
        failwith("Length has to be equal for both code to crack and attempt.")
    // List.map mappingFunction listItself
    attemptList
    |> List.mapi (fun idx color ->
        if color = codeList.[idx] then FullMatch
        else if List.exists (fun color' -> color' = color) codeList then PartialMatch
        else NoMatch)

let isGameWon model =
    match model.Attempts with
    | [] -> false
    | attempts ->
        List.last attempts
        |> (=) model.ToCrack