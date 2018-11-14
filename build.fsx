#r "paket: groupref netcorebuild //"
#load ".fake/build.fsx/intellisense.fsx"

#nowarn "52"

open Fake.Core
open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.IO
open Fake.IO.Globbing.Operators
open Fake.JavaScript

Target.create "Clean" (fun _ ->
    !! "src/bin"
    ++ "src/obj"
    ++ "output"
    |> Seq.iter Shell.cleanDir
)

Target.create "Install" (fun _ ->
    DotNet.restore
        (DotNet.Options.withWorkingDirectory __SOURCE_DIRECTORY__)
        "MasterMind.sln"
)

Target.create "YarnInstall" (fun _ ->
    Yarn.install id
)

let runDotNet cmd workingDir =
    let result =
        DotNet.exec (DotNet.Options.withWorkingDirectory workingDir) cmd ""
    if result.ExitCode <> 0 then failwithf "'dotnet %s' failed in %s" cmd workingDir

Target.create "Build" (fun _ ->
    runDotNet "fable webpack --port free -- -p" __SOURCE_DIRECTORY__
)

Target.create "Watch" (fun _ ->
    let webpackDevServer =
        async {
            runDotNet "fable webpack-dev-server --port free" __SOURCE_DIRECTORY__
        }

    let fableTestWatch =
        async {
            runDotNet "fable yarn-run build-test --port free -- --watch" __SOURCE_DIRECTORY__
        }

    let jestWatch =
        async {
            Yarn.exec "test --watchAll" id
        }

    [webpackDevServer;fableTestWatch;jestWatch]
    |> Async.Parallel
    |> Async.RunSynchronously
    |> ignore
)

Target.create "Tests" (fun _ ->
    runDotNet "fable yarn-run build-test --port free" __SOURCE_DIRECTORY__
    Yarn.exec "test" id
)

// Build order
"Clean"
    ==> "Install"
    ==> "YarnInstall"
    ==> "Build"

"Watch"
    <== [ "YarnInstall" ]

"Tests"
    <== [ "YarnInstall" ]

// start build
Target.runOrDefault "Build"
