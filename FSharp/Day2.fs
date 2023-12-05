namespace AoC

open System
open System.IO

open AoC.General

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
        
        
        
    
    
        