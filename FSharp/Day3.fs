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
    let addElement key v st = 
        st @ [(key, v)]      
    let updateElement key f st = 
        st
        |> List.map (fun (k, v) ->
                        if k = key then
                            k, f v
                        else
                            k, v)
        
      
        
    let groupNumbers (l:int list) =
        
        let rec loop (l: int list) index (acc: (int * int list) list) accIdx : (int * int list) list =
            if index < l.Length then
                
                printfn $"l[{index}]: {l[index]} acc: %A{acc}"
                
                if l[index]-l[index-1] = 1 then                                        
                    let nextAcc = updateElement accIdx (fun v -> v @ [l[index-1]]) acc
                    loop l (index+1) nextAcc accIdx
                else
                    let accUpdateLast = updateElement accIdx (fun v -> v @ [l[index-1]]) acc
                    let nextAcc = addElement (accIdx+1) [] accUpdateLast
                    loop l (index+1) nextAcc (accIdx+1)
            else
                let accUpdateLast = updateElement accIdx (fun v -> v @ [l[index-1]]) acc
                accUpdateLast                                     
            
        loop l 1 [(0,[])] 0 
            
    let findNumbers (s:string) =
        let indexes, numbers =
            s
            |> Seq.indexed
            |> Seq.filter (fun (i,c) -> Char.IsDigit(c))
            |> Seq.toList
            |> List.unzip
         
        indexes,numbers           
        
    let getNeighbors (matrix: 'a[,]) (row: int) (colLow: int) (colHigh:int) =
        let numRows = Array2D.length1 matrix
        let numCols = Array2D.length2 matrix

        let isValid r c =
            r >= 0 && r < numRows && c >= 0 && c < numCols

        let offsets = [-1;1]
        let borderHigh = min (colHigh + 1) (numCols - 1)
        let borderLow = max (colLow - 1) 0 
        let cols = List.init (borderHigh - borderLow + 1 ) (fun v -> v + borderLow)
        printfn $"%A{cols}"
        seq { for dr in offsets do
                for dc in cols do
                    if isValid (row + dr) dc  then
                            // printfn $"M[{row + dr}, {colLow + dc}]: {matrix.[row + dr, colLow + dc]}"
                            yield matrix.[row + dr, dc]
              if isValid row (colLow - 1) then                         
                        yield matrix.[row, colLow - 1]
              if isValid row (colHigh + 1) then                         
                        yield matrix.[row, colHigh + 1]                        
            }
        
        
        
        