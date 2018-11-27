module MasterMind.Test

open Fable.Import.Jest (*jest = testing framework*)
open Fable.Import.Jest.Matchers
open App.Types
open App.State
open App.Selector

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

test "Submitted attempt should be added" <| fun () ->
        let model' = update SubmitAttempt initial
        List.length model'.Attempts == 1

test "Reset should return to initial state" <| fun () ->
        let reset = update ResetGame initial
        reset == initial

test "Correct attempt should return 4x FullMatch pins" <| fun () ->
        let codeToCrack = Red, Red, Red, Red
        let attempt = codeToCrack
        let expectedResults = [ FullMatch; FullMatch; FullMatch; FullMatch ]
        // expect.Invoke(calculatePins codeToCrack attempt).toBe(expectedResults) is the same as ==
        (calculatePins codeToCrack attempt) == expectedResults

test "Invalid attempt should return 4x noMatch pins" <| fun () ->
        let codeToCrack = Red, Red, Red, Red
        let attempt = Violet, Violet, Violet, Violet
        let expectedResults = [ NoMatch
                                NoMatch
                                NoMatch
                                NoMatch ]
        (calculatePins codeToCrack attempt) == expectedResults

test "Invalid attempt should return 1x PartialMatch pins" <| fun () ->
        let codeToCrack = Red, Violet, Blue, Green
        let attempt = Orange, Orange, Orange, Red
        let expectedResults = [ NoMatch
                                NoMatch
                                NoMatch
                                PartialMatch ]

        (calculatePins codeToCrack attempt) == expectedResults

test "Won game on 4th attempt" <| fun () ->
    let model =
        { initial with
            CurrentAttempt = Red, Green, Blue, Violet
            Attempts = [ Red, Red, Red, Red
                         Red, Red, Red, Red
                         Red, Red, Red, Red
                         Red, Green, Blue, Violet ]
            ToCrack = Red, Green, Blue, Violet }

    isGameWon model == true

test "Won game on 0th attempt" <| fun () ->
    let model =
        { initial with
            Attempts = []
            ToCrack = Red, Green, Blue, Violet }

    isGameWon model == false