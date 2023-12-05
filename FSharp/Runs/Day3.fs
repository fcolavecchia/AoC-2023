module Runs.Day3


open NUnit.Framework

open AoC.Day3  


[<Test>]
let ``Day 3: find numbers in a row`` () =
    let s = "467..114.."

    let numbers = findNumbers s
    printfn $"%A{numbers}"
    


[<Test>]
let  ``Day 3 Read Test `` () =
    
    let testFile = fileName + ".test"
    
    let bluePrint = createMatrix testFile        
    printfn "%A" bluePrint
    // let repeatedItems =  getRepeatedItem items   
    //      
    // printfn "%A" repeatedItems
    //
    // let points = getPoints items
    // let expected = 157
    //
    // Assert.AreEqual(expected,points)
    
//
// [<Test>]
// let  ``Day 3`` () =
//     
//     let testFile = fileName
//     
//     let items = getItems testFile        
//     printfn "%A" items 
//     let repeatedItems =  getRepeatedItem items   
//          
//     printfn "%A" repeatedItems
//     
//     let points = getPoints items
//     let expected = 7428
//     
//     Assert.AreEqual(expected,points)
//     
// [<Test>]
// let  ``Day 3 Test, Part 2`` () =
//     
//     let testFile = fileName + ".test"
//     
//     let items = Part2.getItems testFile        
//     printfn "%A" items 
//     let repeatedItems =  Part2.getRepeatedItem items   
//          
//     printfn "%A" repeatedItems
//     
//     let points = Part2.getPoints items
//     let expected = 70
//     
//     Assert.AreEqual(expected,points)
//     
// [<Test>]
// let  ``Day 3, Part 2`` () =
//     
//     let testFile = fileName
//     
//     let items = Part2.getItems testFile        
//     printfn "%A" items 
//     let repeatedItems =  Part2.getRepeatedItem items   
//          
//     printfn "%A" repeatedItems
//     
//     let points = Part2.getPoints items
//     let expected = 70
//     
//     Assert.AreEqual(expected,points)        