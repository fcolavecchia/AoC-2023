namespace AoC

open System
open System.IO

open AoC.General

module Day1 =
    
    let fileName = "day1.input"
 
    type Calibration = string
    type Calibrations = Calibration list 
    
    let writtenNumbers = ["one"; "two"; "three"; "four"; "five"; "six"; "seven"; "eight"; "nine"]        
    
    let numbers = [0;1;2;3;4;5;6;7;8;9] |> List.map (fun n -> sprintf "%d" n)

    let rec countSubstrings (input: string) (substring: string)  =
      match input.IndexOf substring with
      | -1 -> [(-1,substring)]
      | n -> (n,substring) :: countSubstrings (input.Substring(n + 1)) substring     
            
    let findAllSubstringMatches (inputString: string) (substring: string) =
        let rec findMatches (input: string) (sub: string) (startIndex: int) =
            let index = input.IndexOf(sub, startIndex, StringComparison.Ordinal)
            if index >= 0 then
                (index, sub) :: findMatches input sub (index + 1)
            else
                []

        findMatches inputString substring 0            
            
    let findNumbers (numberList: string list) (calibration: Calibration) =
        numberList
        // |> List.map (fun n -> (calibration.IndexOf(n),n))
        |> List.map (fun n -> findAllSubstringMatches calibration n)
        |> List.concat
        |> List.filter (fun (n,_) -> n <> -1)
        
    let stripNumbers (calibration: Calibration) =
        let wNumbers = findNumbers writtenNumbers calibration
        let numbers = findNumbers numbers calibration
        wNumbers @ numbers
        |> List.sortBy fst
        
    let getNumberValue (writtenNumber:string) =
        let index =
            writtenNumbers
            |> List.findIndex (fun s -> s = writtenNumber)
        
        index+1 
        
        
    let transformToInt (number:string) =
        if (number.Length = 1) then
            number |> Seq.toList |> List.head |> Char.GetNumericValue |> int 
        else
            getNumberValue number 
        
    let createCalibration2 (calList: (int * string) list) =
        let first, last  =
            match calList.Length with
                | 0 -> (0,"0"), (0,"0")
                | 1 -> calList[0] ,  calList[0]
                | 2 -> calList[0],  calList[1]
                | _ -> calList[0],  calList[calList.Length-1] 
        let dec = first |> snd |> transformToInt
        let un = last |> snd |> transformToInt
        dec * 10 + un 
    
    // Part 1
    
    let stripChars (calibration: Calibration) : int list  = 
        calibration
        |> Seq.filter (fun c -> Char.IsDigit(c))
        |> Seq.map (fun c -> System.Char.GetNumericValue c |> int)
        |> Seq.toList
        
        
    let createCalibration (calList: int list) =
        match calList.Length with
        | 0 -> 0
        | 1 -> calList[0] *10 + calList[0]
        | 2 -> calList[0] * 10 + calList[1]
        | _ -> calList[0] * 10 + calList[calList.Length-1] 
        
        
    
    
    let chunkBySpace l =
        
        let rec loop n list outList =
            let idxOpt = list
                            |> List.tryFindIndex (fun s -> String.IsNullOrEmpty(s) )
            match idxOpt with
            | Some idx -> 
                let s = list.[0..idx-1]
                let tail = list.[idx+1..] 
                
                let nextList =  List.append outList [(n,s)] 
                loop (n+1) tail nextList 
            | None ->
                List.append outList [(n,list)] 
        loop 0 l []
                                
    let elfCalories lines =
        chunkBySpace lines
        |> List.map (fun (i,s) -> (i,s |> List.map int)) 
        
    // let maxElfCalories (elves: Elves) =
    //     elves
    //     |> List.map (fun (i,s) ->  List.sum s)
    //     |> List.max
    //     
    // let top3ElfCalories (elves: Elves) =
    //     elves
    //     |> List.map (fun (i,s) ->  List.sum s)
    //     |> List.sortDescending
    //     |> List.take 3         
        
        