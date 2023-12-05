namespace AoC

open System
open System.IO

module Day3 =
    
    let fileName = "day3.input"
        
    
    let listOfArraysToArray2D (rows: char[] list) =
        // let rows = inputList |> List.map Array.ofList
        let numRows = List.length rows
        let numCols =
            match rows with
            | [] -> 0
            | head::_ -> Array.length head
        
        Array2D.init numRows numCols (fun i j -> rows.[i].[j])
    
    let createMatrix fileName =
        let lines = AoC.General.readInput fileName
        
        let rows =
            lines
            |> List.map (fun s -> s |> Seq.toList |> List.toArray)
        
        listOfArraysToArray2D rows 
        

    // let findNumbers (rowIndex: int) (matrix: char[,]) =
    //     let row = matrix[rowIndex..rowIndex,*]
    //     row
        
    // let findNumbers (s: string) =
    //     
    //     let rec loop (s:string) acc (index:int)  =
    //         
    //         if index < s.Length then 
    //             if s[index]<>'.' then                    
    //                 s[index] :: loop s acc (index+1)
    //             else
    //                 loop s acc (index+1)
    //         else
    //             acc 
    //             
    //     loop s [] 0                 
                
    let findNumbers (s:string) =
        s
        |> Seq.mapi (fun i c -> (i,c))
        |> Seq.filter (fun (i,c) -> c <> '.')
        
        
        
        