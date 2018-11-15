module MasterMind.Test

open Fable.Import.Jest (*jest = testing framework*)
open Fable.Import.Jest.Matchers
open App.Types
open App.State

let initial = init()

test "Init Should Not Be Null" <| fun () ->
    expect.Invoke(initial).not.toBeNull()

test "Init Should be 4x Red"  <| fun () ->
    let model = init()
    match model.CurrentAttempt with
    | Red, Red, Red, Red -> ()
    | _ -> failwith "Unexpected attempt"
    
test "Change color should take the next color" <| fun () ->
    let model' = update (Msg.ChangeColor 0) initial
    
    match model'.CurrentAttempt with
    | Green, Red, Red, Red -> ()
    | _ -> failwithf "First color should be red"
    

test "Change color should take max index into account" <| fun () ->
    let model = { initial with CurrentAttempt = Violet, Red, Red, Red }
    let model' = update (Msg.ChangeColor 0) model
    initial.CurrentAttempt == model'.CurrentAttempt // (==) same as expect.Invoke(initial.CurrentAttempt).toEqual(model'.CurrentAttempt)
    
test "Third color should change" <| fun () ->
    let model = { initial with CurrentAttempt = Red, Red, Yellow, Red }
    let model' = update (Msg.ChangeColor 2) model
    let (_,_, orange, _) = model'.CurrentAttempt
    orange == Orange
    