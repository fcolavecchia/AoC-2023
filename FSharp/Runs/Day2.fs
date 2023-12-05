module Runs.Day2


open NUnit.Framework

open AoC
open Day2


[<Test>]
let  ``Day 2 Read Test`` () =
    
    let testFile = Day2.fileName + ".test"
    
    let lines = General.readInput testFile
    printfn "%A" lines       
                
    let actual = lines
                |> List.map Day2.readGame
    
    printfn "%A" actual 
    
    let expected : Day2.Game list =
        [
         [{ Red = Some 4
            Green = None
            Blue = Some 3 }; { Red = Some 1
                               Green = Some 2
                               Blue = Some 6 }; { Red = None
                                                  Green = Some 2
                                                  Blue = None }];
        [{  Red = None
            Green = Some 2
            Blue = Some 1 }; { Red = Some 1
                               Green = Some 3
                               Blue = Some 4 }; { Red = None
                                                  Green = Some 1
                                                  Blue = Some 1 }];
        [{  Red = Some 20
            Green = Some 8
            Blue = Some 6 }; { Red = Some 4
                               Green = Some 13
                               Blue = Some 5 }; { Red = Some 1
                                                  Green = Some 5
                                                  Blue = None }];
        [{  Red = Some 3
            Green = Some 1
            Blue = Some 6 }; { Red = Some 6
                               Green = Some 3
                               Blue = None }; { Red = Some 14
                                                Green = Some 3
                                                Blue = Some 15 }];
        [{  Red = Some 6
            Green = Some 3
            Blue = Some 1 }; { Red = Some 1
                               Green = Some 2
                               Blue = Some 2 }]
        ]

        
    Assert.AreEqual(expected,actual)
    
[<Test>]
let  ``Day 2 Test: isValidReveal`` () =
    
    let invalidCube =
        { Red = Some 20
          Green = Some 8
          Blue = Some 6 }
    let validCube =
        { Red = Some 2
          Green = Some 8
          Blue = Some 6 }
    let validCubeWithNone =
        { Red = Some 2
          Green = None 
          Blue = Some 6 }        
        
        
    let actualInvalid = isValidReveal invalidCube
    let actualValid = isValidReveal validCube
    let actualValidWithNone = isValidReveal validCubeWithNone
    printfn "%A" actualInvalid
    printfn "%A" actualValid
    printfn "%A" actualValidWithNone

    Assert.AreEqual(false,actualInvalid)
    Assert.AreEqual(true,actualValid)
    Assert.AreEqual(true,actualValidWithNone)
    
    
    
[<Test>]
let  ``Day 2 Test, Part 1`` () =
          
    let testFile = Day2.fileName + ".test"
    
    let lines = General.readInput testFile
                
    let actual = lines
                |> List.mapi (fun i l -> i+1,Day2.readGame l)
                |> List.map (fun (i,g) ->  i,Day2.isValidGame g)
                |> List.filter(fun (i,g) -> g=true)
    let expected = [1;2;5]
    printfn "%A" actual        
    Assert.AreEqual(expected,actual |> List.map fst)                

[<Test>]
let  ``Day 2, Part 1`` () =
     
    let testFile = Day2.fileName 

    let lines = General.readInput testFile
                           
    let actual = lines
                |> List.mapi (fun i l -> i+1,Day2.readGame l)
                |> List.map (fun (i,g) ->  i,Day2.isValidGame g)
                |> List.filter(fun (i,g) -> g=true)
                |> List.map fst
                |> List.sum 
    
    let expected = 2449
    
    printfn "%A" actual        
    Assert.AreEqual(expected,actual)      


[<Test>]
let  ``Day 2 Test, Part 2: power`` () =
          
    let testFile = Day2.fileName + ".test"
    
    let lines = General.readInput testFile
                
    let actual = lines
                |> List.mapi (fun i l -> i+1,Day2.readGame l)
                |> List.map (fun (i,g) ->  i,computeMinCubesForGame g |> power)
    let expected = [48; 12; 1560; 630; 36]
    printfn "%A" actual        
    Assert.AreEqual(expected,actual |> List.map snd)
    
[<Test>]
let  ``Day 2 Test, Part 2: sum all`` () =
          
    let testFile = Day2.fileName + ".test"
    
    let lines = General.readInput testFile
                
    let actual = lines
                |> List.mapi (fun i l -> i+1,Day2.readGame l)
                |> List.map (fun (i,g) ->  computeMinCubesForGame g |> power)
                |> List.sum 
    let expected = 2286
    printfn "%A" actual        
    Assert.AreEqual(expected,actual)
    
[<Test>]
let  ``Day 2, Part 2`` () =
          
    let testFile = Day2.fileName
    
    let lines = General.readInput testFile
                
    let actual = lines
                |> List.mapi (fun i l -> i+1,Day2.readGame l)
                |> List.map (fun (i,g) ->  computeMinCubesForGame g |> power)
                |> List.sum 
    let expected = 63981
    printfn "%A" actual        
    Assert.AreEqual(expected,actual)                                    