module App.Types

type Color = // enum
    | Red
    | Green
    | Blue
    | Yellow
    | Orange
    | Violet

type Pin =
    | NoMatch
    | PartialMatch
    | FullMatch

type Attempt = Color * Color * Color * Color // Tuple

type Code = Attempt

type Model =
    { ToCrack : Code
      CurrentAttempt : Attempt
      Attempts : Attempt list }

type Msg =
    | ChangeColor of int // position in current attempt
    | SubmitAttempt
    | ResetGame

[<Literal>]
let MaxAttempts = 10
