module App.Types

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