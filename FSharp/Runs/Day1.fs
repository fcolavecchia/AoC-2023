module Runs.Day1 

open NUnit.Framework

open AoC


[<Test>]
let  ``Day 1 Test`` () =
    
    let testFile = Day1.fileName + ".test"
    
    let lines = General.readInput testFile

    printfn "%A" lines
    
    let expected = [ [1;2]; [3;8]; [1;2;3;4;5]; [7] ]
    
    let actual = lines
              |> List.map Day1.stripChars   
    printfn "%A" actual 

    Assert.AreEqual(expected,actual)
    
[<Test>]
let rec  ``Day 1 Test2`` () =
    
    let testFile = Day1.fileName + ".test"
    
    let lines = General.readInput testFile

    printfn "%A" lines
    
    let expected = [ 12; 38; 15; 77 ]
    
    let actual = lines
              |> List.map Day1.stripChars
              |> List.map Day1.createCalibration 
    printfn "%A" actual 

    Assert.AreEqual(expected,actual)
        
[<Test>]
let rec  ``Day 1`` () =
    
    let testFile = Day1.fileName
    
    let lines = General.readInput testFile

    // printfn "%A" lines
    
    let expected = 0
    
    let actual = lines
              |> List.map Day1.stripChars
              |> List.map Day1.createCalibration
              |> List.sum 
    printfn "%A" actual 

    // Assert.AreEqual(expected,actual)
        
        
// Part 2
[<Test>]
let  ``Day 1 Part 2 Test`` () =
    
    let testFile = Day1.fileName + ".Part2.test"
    
    let lines = General.readInput testFile

    printfn "%A" lines
    
    let expected = [["two"; "1"; "nine"]; ["eight"; "two"; "three"]; ["one"; "2"; "three"];
                     ["two"; "one"; "3"; "four"]; ["4"; "nine"; "eight"; "seven"; "2"];
                     ["one"; "eight"; "2"; "3"; "4"]; ["7"; "six"];
                     ["8"; "1"; "seven"; "9";  "two"; "1"]; ["9"; "six"; "seven"; "4"; "9"];
                     ["six"; "7"; "one"; "six"]]
    
    let actual = lines
              |> List.map Day1.stripNumbers
              |> List.map (fun l -> l |> List.map snd)
    printfn "%A" actual 

    Assert.AreEqual(expected,actual)
    
[<Test>]
let  ``Day 1 Part 2 Test 2`` () =
    
    let testFile = Day1.fileName + ".Part2.test"
    
    let lines = General.readInput testFile

    printfn "%A" lines
    
    let expected = [29; 83; 13; 24; 42; 14; 76; 81; 99; 66]
    
    let actual = lines
                 |> List.map Day1.stripNumbers
                 |> List.map Day1.createCalibration2
              
    printfn "%A" actual 

    Assert.AreEqual(expected,actual)            

[<Test>]
let  ``Day 1 Part 2`` () =
    
    let testFile = Day1.fileName //+ ".Part2.test"
    
    let lines = General.readInput testFile

    // printfn "%A" lines
    
    // let expected = [29; 83; 13; 24; 42; 14; 76]
    
    let actual = lines
                 |> List.map (fun s -> s,Day1.stripNumbers s)
                 |> List.map (fun (s,l) -> (s,Day1.createCalibration2 l))
                 
    // actual
    // |> List.iter (fun (s,v) -> printfn $"{s}: {v}")
    // printfn "%A" actual
    printfn "%A" (actual |> List.map snd |> List.sum)

    // Assert.AreEqual(expected,actual)            

// [<Test>]
// let  ``Day 1`` () =
//     
//     let testFile = Day1.fileName 
//     
//     let lines = Day1.readInput testFile
//     
//     let elves = Day1.chunkBySpace lines
//     
//     // printfn "%A" lines
//     // printfn "%A" elves
//
//     // Part 1  
//     let answerPart1 = 69626
//     let m = Day1.maxElfCalories (Day1.elfCalories lines) 
//     printfn "%A" m
//
//     // Part 2
//     let top3 = Day1.top3ElfCalories (Day1.elfCalories lines)
//     let top3Expected = [69626; 68657; 68497]
//     let top3Sum = (top3 |> List.sum)
//     printfn "%A"  top3Sum 
//     
//     let answerPart2 = 206780
//     Assert.AreEqual(top3Expected,top3)
//     Assert.AreEqual(top3Sum,answerPart2)
//     Assert.AreEqual(answerPart1,m)    
