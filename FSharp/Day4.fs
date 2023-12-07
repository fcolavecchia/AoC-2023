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
        
    let computeNumberOfWins winning mine =
        
        let numberOfWins =
            winning
            |> Seq.filter (fun w ->
                        Seq.contains w mine 
            )
            |> Seq.distinct
            |> Seq.length
            
        numberOfWins
         
    let computeWinners numberOfWins =        
        match numberOfWins with
        | 0 -> 0.0
        | 1 -> 1.0 
        | _ -> 2.0 ** (float numberOfWins  - 1.0)
        
    // Part 2
    
    let computeRecursiveNumberOfWins (allWins: (int * int) list) =
        
        let nGames = allWins.Length 
                
            
        let wins = allWins |> List.map snd             
        let acc = List.replicate nGames 0 |> List.toArray
        
        printfn $"acc: %A{acc}"
        
        // Add original cards  
        for n = 0 to nGames - 1 do
            acc[n] <- acc[n] + 1
        printfn $"dealt acc: %A{acc}"

        // Process the win value for each card
        for n = 0 to nGames - 1 do
            for i = 0 to wins[n]-1 do
                acc[i + n + 1] <- acc[i + n + 1] + 1 
            
        printfn $"original acc:%A{acc}"    

        // Process copies
        for n = 1 to nGames - 1 do
            for i = 0 to wins[n]-1 do
                acc[i + n + 1] <- acc[i + n + 1] + acc[n] - 1  
                
        acc                 
            
        
        
        
        
                        
        // loop 0 wins localWins acc  
        
        
        
        