module Runs.Day2


open NUnit.Framework

open AoC 


[<Test>]
let  ``Day 2 Read Test`` () =
    
    let testFile = Day2.fileName + ".test"
    
    let lines = General.readInput testFile
    printfn "%A" lines[0]
    
    // let games = lines
    //             |> List.map Day2.readGame
                
    let games = lines[0] |> Day2.readGame
    
    printfn "%A" games
    
        
    // Assert.AreEqual(expected,points)
    
// [<Test>]
// let  ``Day 2`` () =
//     
//     let testFile = Day2.fileName
//     
//     let rounds = Day2.roundsPart1 testFile
//     
//     // let elves = Day2.chunkBySpace rounds
//     
//     printfn "%A" rounds
//
//     let expected = 15572    
//     //
//     let points = Day2.getPoints rounds 
//     printfn "%A" points
//
//     Assert.AreEqual(expected,points)
//     
// [<Test>]
// let  ``Day 2 Test, Part 2`` () =
//     
//     let testFile = Day2.fileName + ".test"
//     
//     let rounds = Day2.roundsPart2 testFile        
//     printfn "%A" rounds
//     let expected = 12    
//     
//     let points = Day2.getPoints rounds 
//     printfn "%A" points
//         
//     Assert.AreEqual(expected,points)
//     
// [<Test>]
// let  ``Day 2, Part 2 `` () =
//     
//     let testFile = Day2.fileName
//     
//     let rounds = Day2.roundsPart2 testFile
//     
//     // let elves = Day2.chunkBySpace rounds
//     
//     printfn "%A" rounds
//
//     let expected = 16098   
//     //
//     let points = Day2.getPoints rounds 
//     printfn "%A" points
//
//     Assert.AreEqual(expected,points)        