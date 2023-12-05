namespace AoC

open System
open System.IO

module Day2 =
    
    let fileName = "day2.input"
    
    type Cubes =
         {
             Red : int option
             Green : int option 
             Blue : int option
         }

    type Game = Cubes list
    
    let emptyCube =
        {
            Red = None
            Green = None
            Blue = None 
        }
    
    let maxCube =
        {
            Red = Some 12
            Green = Some 13
            Blue = Some 14 
        }
        
    let compareOptions (opt1: int option) (opt2: int option) (operator: int -> int -> bool) =
        match opt1,opt2 with
        | Some v1, Some v2 -> operator v1  v2 
        | None, Some v2 -> true
        | Some v1, None -> true
        | None, None -> false 

        
    let addColoredCube (cubes:Cubes option) (coloredCube: string)  = // 3 blue
        let splitted = coloredCube.Split(" ")
        let number = splitted[0] |> General.stringToInt
        let color = splitted[1]
        // printfn $"number: {number} color:{color}"
        let newCube =
            match cubes with
            | Some cube -> 
                match color with
                | "blue" -> {cube with Blue = number} |> Some 
                | "red" -> {cube with Red = number} |> Some 
                | "green" -> {cube with Green = number} |> Some 
                | _ -> None 
            | None -> None         
        // printfn $"newCube: {newCube}"
        newCube
        
    let readRevealed (revealedString:string) =
        let coloredCubes = revealedString.Split(",")
                           |> Seq.toList
                           |> List.map (fun s -> s.Trim())
        // printfn $"coloredCubes: {coloredCubes}"
        coloredCubes
        |> List.fold (fun cube c -> addColoredCube cube c) (Some emptyCube)        
                       
    let readGame (gameString: string): Game =
        let game = gameString.Split(":")[1]
        // printfn  $"{game}"
        game.Split(";")
        |> Seq.toList
        |> List.map readRevealed
        |> List.choose id 
        
    let isValidReveal cube =
        compareOptions cube.Red maxCube.Red (<=) &&
        compareOptions cube.Green maxCube.Green (<=) &&
        compareOptions cube.Blue maxCube.Blue (<=)
        // cube < maxCube: Does not work right, need to see what is going on
        
    let isValidGame game =
        game
        |> List.forall isValidReveal
        
    let getMaxFromIntOptionList (lOpt: int option list) =
        let l =
            lOpt
            |> List.choose id
        if l.IsEmpty then
            None
        else
            Some (List.max l)
        
        
    let computeMinCubesForGame (game: Game ) =
        
        {
            Red = game |> List.map (fun g -> g.Red) |> getMaxFromIntOptionList
            Green = game |> List.map (fun g -> g.Green) |> getMaxFromIntOptionList
            Blue = game |> List.map (fun g -> g.Blue) |> getMaxFromIntOptionList
        }
            
    let power cube =
        let value i =
            match i with
            | Some v -> v
            | None -> 1
            
        (value cube.Red) * (value cube.Green) * (value cube.Blue) 