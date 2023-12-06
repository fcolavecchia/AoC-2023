namespace AoC

open System
open System.IO

module Day4 =
    
    let fileName = "day4.input"
    
    let readCard (s:string) =
        
        let game = s.Split(":")[1]
        
        let w = game.Split('|')
        let winning = w[0].Trim() |> Seq.map (fun w -> w. <> " ") 
        let mine = w[1].Trim().Split(' ') |> Seq.filter (fun w -> w <> " ")
        
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