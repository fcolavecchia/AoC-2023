namespace AoC

open System
open System.IO

module Day4 =
    
    let fileName = "day4.input"
    
    let readCard (s:string) =
        
        let game = s.Split(":")[1]
        
        let w = game.Split('|')
        let winning = w[0]
                      |> Seq.chunkBySize 3
                      |> Seq.map (fun a -> a |> String)
                      |> Seq.map (fun s -> s.Trim())
                      
        let mine = w[1]
                    |> Seq.chunkBySize 3
                    |> Seq.map (fun a -> a |> String)
                    |> Seq.map (fun s -> s.Trim())
        printf $"w: %A{winning}"
        printfn $"m: %A{mine}"
        winning,mine
        
    let computeWinners winning mine =
        
        let numberOfWins =
            winning
            |> Seq.filter (fun w ->
                        Seq.contains w mine 
            )
            |> Seq.distinct
            |> Seq.length 
        match numberOfWins with
        | 0 -> 0.0
        | 1 -> 1.0 
        | _ -> 2.0 ** (float numberOfWins  - 1.0)  