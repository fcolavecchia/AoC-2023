namespace AoC

open System
open System.IO

module Day5 =
    
    let fileName = "day5.input"
    
    type Range =
        {
            Start: int64
            Span: int64 
        }
        
    type Source = Range
    type Destination = Range 
        
    let readTriplet (s: string) =
        let t = s.Split(' ')
        
        [t[0]; t[1]; t[2]]
        
    let tripletToSrcDest s d span  =
        let source =
            {
                Start = s
                Span = span  
            }
        let destination =
            {
                Start = d
                Span = span  
            }            
        source,destination
        
    let readSeeds (lines: string) =     
        let seedStr = lines.Split(':')[1]
        
        seedStr.Trim().Split(' ')
        |> Seq.toList
        |> List.map General.stringToInt64
        |> List.choose id 
        
        
    type MapTypes =
        | SeedToSoil 
        | SoilToFertilizer
        | FertilizerToWater
        | WaterToLight
        | LightToTemperature
        | TemperatureToHumidity
        | HumidityToLocation
        static member FromText s =
            match s with
            | "seed-to-soil map" -> SeedToSoil
            | "soil-to-fertilizer map" -> SoilToFertilizer
            | "fertilizer-to-water map" -> FertilizerToWater
            | "water-to-light map" -> WaterToLight
            | "light-to-temperature map" -> LightToTemperature
            | "temperature-to-humidity map" -> TemperatureToHumidity
            | "humidity-to-location map" -> HumidityToLocation
            | _ -> raise(Exception "Map not found")
                        
            
    type SeedMap =
        {
            MapType : MapTypes
            Ranges: (Source * Destination) list
            
        }
            
    let readMap (map: string list) =
        let mapType = map[0].Split(':')[0] |> MapTypes.FromText
        let ranges = map |> List.skip 1
        
        let getSrcDest s =
            let triplet =
                s
                |> readTriplet
                |> List.map General.stringToInt64
                |> List.choose id
            tripletToSrcDest triplet[1] triplet[0] triplet[2]

        {
            MapType = mapType
            Ranges =
                ranges
                |> List.map getSrcDest                  
        }
        
        
        
    let splitIntoMaps (lines: string list) =
        
        let rec loop (inList: string list) (outList: SeedMap list) =
            
            if (List.isEmpty(inList) |> not) then 
                let splitIndex = inList
                                 |> List.tryFindIndex (fun s -> s = "")
                match splitIndex with
                | Some index -> 
                    let map = List.take index inList |> readMap
                    let tail = List.skip (index+1) inList 
                    printfn $"map: %A{map}"
                    let mapList =  List.append outList [map]  
                    loop tail mapList
                | None ->
                    let map = inList |> readMap
                    let mapList =  List.append outList [map]
                    mapList

            else
                outList
        loop lines []
        
        
    let transform mapType maps inValue =
        
        let mapIsValid =
            maps
            |> List.tryFind (fun v -> v.MapType = mapType)
        
        match mapIsValid with
        | Some m ->
            let isInRange =
                m.Ranges
                |> List.tryFind(fun (s,_) ->
                                s.Start <= inValue && inValue <= (s.Start + s.Span - 1L) )
            
            let outValue =
                match isInRange with
                | Some (s,d) ->
                    inValue - s.Start + d.Start 
                | None ->
                    inValue 
            
            printfn $"{mapType}: {outValue}"
            outValue
            
        | None -> raise(Exception "Map is not a valid transformation")
        
        
        
    let seedToLocation maps seed =
            
        seed
        |> transform MapTypes.SeedToSoil maps
        |> transform MapTypes.SoilToFertilizer maps
        |> transform MapTypes.FertilizerToWater maps
        |> transform MapTypes.WaterToLight maps
        |> transform MapTypes.LightToTemperature maps
        |> transform MapTypes.TemperatureToHumidity maps
        |> transform MapTypes.HumidityToLocation maps 
            
            
    module Part2 =
        let readSeedsAsRanges (lines: string) =     
            let seedStr = lines.Split(':')[1]
            
            seedStr.Trim().Split(' ')
            |> Seq.toList
            |> List.map General.stringToInt64
            |> List.choose id
            |> List.pairwise
            |> List.indexed
            |> List.filter (fun (i, _) -> i%2 = 0)
            |> List.map snd
            |> List.map (fun (s,span) ->
                            {
                                Start = s
                                Span = span 
                            })
            
            
        let seedToLocation maps range =
            
            let mutable maxLocation = Int64.MaxValue
            
            
            for seed in range.Start..(range.Start + range.Span - 1L) do 
            
                let location = 
                    seed
                    |> transform MapTypes.SeedToSoil maps
                    |> transform MapTypes.SoilToFertilizer maps
                    |> transform MapTypes.FertilizerToWater maps
                    |> transform MapTypes.WaterToLight maps
                    |> transform MapTypes.LightToTemperature maps
                    |> transform MapTypes.TemperatureToHumidity maps
                    |> transform MapTypes.HumidityToLocation maps             
                
                maxLocation <- min maxLocation location
                
            maxLocation 
                
                
            
            