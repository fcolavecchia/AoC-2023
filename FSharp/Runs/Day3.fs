module Runs.Day3


open NUnit.Framework

open AoC.Day3  


[<Test>]
let ``Day 3: find numbers in a row`` () =
    let s = "467..114.."

    let expectedIndexes = [0; 1; 2; 5; 6; 7]
    let expectedNumbers = ['4'; '6'; '7'; '1'; '1'; '4']
    
    
    let indexes,numbers = findNumbers s
    printfn $"%A{indexes}"
    printfn $"%A{numbers}"
    Assert.AreEqual(expectedIndexes,indexes)
    Assert.AreEqual(expectedNumbers,numbers)
    

[<Test>]
let ``Day 3: update elements`` () =
    let acc = [(0,[])]
    let expected = [(0,[1])]
    let actual = updateElement 0 (fun l -> l @ [1]) acc
    
    Assert.AreEqual(expected,actual)
    

[<Test>]
let ``Day 3: group Numbers`` () =
    let s = [0;1;2;3;6;7]

    let expected = [(0,[0;1;2;3]);(1,[6;7])]
    
    let actual = groupNumbers s
    printfn $"%A{actual}"
    
    Assert.AreEqual(expected,actual)

[<Test>]
let ``Day 3: get neighbors`` () =
    
    let testFile = fileName + ".test"
    
    let bluePrint = createMatrix testFile
    printfn $"%A{bluePrint}"

    let actual1 = getNeighbors bluePrint 0 0 2
    let expected1 = ['.'; '.'; '.'; '*'; '.']
    printfn "%A" (actual1 |> Seq.toList)
    
    Assert.AreEqual(expected1,actual1)
    
    let actual2 = getNeighbors bluePrint 6 2 4
    let expected2 = ['.'; '.'; '.'; '.'; '+'; '.'; '.'; '.'; '.'; '.'; '.'; '.']
    printfn "%A" (actual2 |> Seq.toList)

    Assert.AreEqual(expected2,actual2)

    let actual3 = getNeighbors bluePrint 8 7 9
    let expected3 = ['7'; '5'; '5'; '.'; '9'; '8'; '.'; '.'; '.']
    printfn "%A" (actual3 |> Seq.toList)

    Assert.AreEqual(expected3,actual3)    
    
    
    
    
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