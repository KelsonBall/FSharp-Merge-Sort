﻿namespace Main
module Program =

    let isLetter (letter:char) =
        if letter >= 'a' && letter <= 'z' then
            true
        else if letter >= 'A' && letter <= 'Z' then
            true
        else
            false

    let rec shiftCharUp (letter:char) shift : char =
        if shift = 0 then
            letter
        else
            if isLetter letter then
                match letter with
                | 'z' -> shiftCharUp 'a' (shift - 1)
                | 'Z' -> shiftCharUp 'A' (shift - 1)
                | _ -> shiftCharUp (letter + (char)1) (shift - 1)
            else
                letter

    let rec encode (mutator: int -> int) (key:int) (content:string)  : string =
        if content.Length = 0 then
            ""
        else
            ((string)(shiftCharUp ((char)content.[0]) key)) + (encode mutator (mutator key) (content.[1..])  )

    let simpleEncode = encode (fun value -> value + 1 )
    let simpleDecode = encode (fun value -> value - 1 )

    [<EntryPoint>]
    let main argv =
        let code = simpleEncode 7 "Hello, World!"
        printfn "%s" code
        let result = simpleDecode (26 - 7) code
        printfn "%s" result
        0 // return an integer exit code