
$ArtifactsPath = "$(pwd)" + "\packages"



function install-freesqlbuilder-ui {
    Push-Location .\src\FreeSqlBuilderUI\Client\FreeSqlBuilderClient\ClientApp
    yarn
    yarn build
    Pop-Location
}


function dotnet-build {
     dotnet build -c Release
}

function dotnet-pack {
    Get-ChildItem -Path .\src\FreeSqlBuilder\** |ForEach-Object {
        echo $(pwd)
        dotnet pack $_ -c Release --no-build -o $ArtifactsPath
    }
     dotnet pack .\src\FreeSqlBuilderUI\FreeSqlBuilderUI -c Release --no-build -o $ArtifactsPath
}

@( "install-freesqlbuilder-ui","dotnet-build", "dotnet-pack" ) | ForEach-Object {
    echo ""
    echo "***** $_ *****"
    echo ""

    # Invoke function and exit on error
    &$_
    if ($LastExitCode -ne 0) { Exit $LastExitCode }
}
