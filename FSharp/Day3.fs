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
                
                // printfn $"l[{index}]: {l[index]} acc: %A{acc}"
                
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
        if List.isEmpty l then
            []
        else 
            loop l 1 [(0,[])] 0 
            
    let findNumbers (s:char[]) =
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
        // printfn $"%A{cols}"
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
        
    let hasSymbolsAround neighbors =
        neighbors
            |> Seq.exists (fun v -> v <> '.')        
        
    let hasAsteriskAround neighbors =
        neighbors
            |> Seq.exists (fun v -> v = '*')        
        
    let getValidPieces neighborsPredicate (matrix: char[,]) =
        
        seq {
            for i = 0 to (Array2D.length1 matrix - 1 ) do
                let row = matrix[i,*]
                // printfn $"row: %A{row}"
                let indexes,numbers = findNumbers row
                let groups = groupNumbers indexes
                if ((List.isEmpty groups) |> not) then 
                    let okNumbers =
                        groups
                        |> List.map (fun (n,l) ->
                                    let neighbors = getNeighbors matrix i l[0] (l[l.Length-1])
                                    let hasSymbolsAround = neighborsPredicate neighbors
                                        // neighbors
                                        // |> Seq.exists (fun v -> v <> '.')
                                    (n,hasSymbolsAround)                                
                                    )
                    (groups,okNumbers)
                    ||> List.map2  (fun (_,g) (_,okn)  ->
                                        if okn then
                                            let number =
                                                g
                                                |> List.map (fun i -> row[i])
                                                |> List.toArray
                                                |> String
                                                |> General.stringToInt
                                            number 
                                        else
                                            None )
            }
        
    let findAdjacentNumbers (matrix: char[,]) (row:int) (col:int) =
        
        let numRows = Array2D.length1 matrix
        let numCols = Array2D.length2 matrix

        let findNumberAtColumn (row: char[]) col =
            
            let indexes,numbers = findNumbers row
            let groups = groupNumbers indexes 
            
            let number =
                groups
                |> List.tryFind (fun (_,indexes) -> List.contains col indexes
                                                    || List.contains (col+1) indexes
                                                    || List.contains (col-1) indexes)
                |> Option.map snd
                |> Option.map (fun l ->
                                    l
                                    |> List.map (fun i -> row[i])
                                    |> List.toArray
                                    |> String
                                    |> General.stringToInt
                                    )
                |> Option.bind id
            number 
        
        let isValid r c =
            r >= 0 && r < numRows && c >= 0 && c < numCols
        
        let offsets = [-1;0;1]
        
        let rowCandidates,colCandidates =
            seq {
                    for r in offsets do
                        for c in offsets do
                            if (isValid (row + r) (col + r) && (r, c) <> (0, 0)) then
                                if Char.IsDigit(matrix[row + r, col + c]) then
                                    yield (row + r,col + c)                                 
            }
            |> Seq.toList
            |> List.unzip
                   
        let candidates =
            (rowCandidates,colCandidates)
            ||> List.map2 (fun r c -> findNumberAtColumn matrix[r, *] c)
            |> List.distinct
            
                           
        if (candidates.Length = 2) then
                        
            match candidates[0],candidates[1] with
                | Some f, Some s -> Some (f,s)
                | _ -> None 
        else 
            None 
        
    let getGearPieces (matrix: char[,]) =
        
        let numRows = Array2D.length1 matrix
        let numCols = Array2D.length2 matrix
    
        seq {
            for i=0 to numRows-1 do                 
                for j=0 to numCols-1 do
                    if (matrix[i,j] = '*') then
                        findAdjacentNumbers matrix i j                         
        }
        |> Seq.toList
        
    let getGearValues gearPieces =
        gearPieces
        |> List.fold (fun acc (f,s) -> acc + f*s) 0 