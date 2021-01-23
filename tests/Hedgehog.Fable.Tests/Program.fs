module Hedgehog.Fable.Tests.Main

#if FABLE_COMPILER
open Fable.Mocha
#else
open Expecto
#endif

open Hedgehog

let smokeTests = testList "Smoke tests" [
    testCase "Smoke tests" <| fun _ ->
        // For now just test if everything compiles
        property {
            let! x = Gen.int (Range.linear 100 200)
            return x = x
        } |> Property.check
]

let allTests = testList "All tests" [
    smokeTests
    TreeTests.treeTests
    RangeTests.rangeTests
    GenTests.genTests
    SeedTests.seedTests
    ShrinkTests.shrinkTests
    MinimalTests.minimalTests
]

[<EntryPoint>]
let main (args: string[]) =
#if FABLE_COMPILER
    Mocha.runTests allTests
#else
    runTestsWithArgs defaultConfig args allTests
#endif
