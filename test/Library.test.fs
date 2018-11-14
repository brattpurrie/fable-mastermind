module MasterMind.Test
open Fable.Import.Jest (*jest = testing framework*)
open App.Types
open App.State

test "Init Should Not Be Null" <| fun () ->
    let model = init()
    expect.Invoke(model).not.toBeNull()

test "Init Should be 4x Red"  <| fun () ->
    let model = init()
    let expectedModel = {
        CurrentAttempt = Color.Red, Color.Red, Color.Red, Color.Red
        Attempts =  List.empty
    }

    expect.Invoke(model).toEqual(expectedModel)

// test "Init Should not be 4x Red"  <| fun () ->
//     let model = init()
//     let expectedModel = {
//         CurrentAttempt = Color.Red, Color.Red, Color.Red, Color.Red
//         Attempts =  List.empty
//     }

//     expect.Invoke(model).toEqual(expectedModel)