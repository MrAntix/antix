Param($version,$apiKey)

if ($version -Eq $null) {
    $version = Read-Host "Enter Version Number"
}

$nugetPath = Split-Path -parent $PSCommandPath
$sourcePath = "$nugetPath\..\source"
$destinationPath = "$nugetPath\Packages\$version"

# Create Package Structure
New-Item -Path "$destinationPath" -Type directory -ErrorAction Stop

# Package
set-alias nuget $sourcePath\.nuget\NuGet.exe

nuget pack "$sourcePath\Antix\Antix.code.nuspec" -Properties version=$version -OutputDirectory "$destinationPath"
nuget pack "$sourcePath\Antix.Data.Keywords\Antix.Data.Keywords.code.nuspec" -Properties version=$version -OutputDirectory "$destinationPath"
nuget pack "$sourcePath\Antix.Data.Keywords.EF\Antix.Data.Keywords.EF.code.nuspec" -Properties version=$version -OutputDirectory "$destinationPath"
nuget pack "$sourcePath\Antix.Drawing\Antix.Drawing.code.nuspec" -Properties version=$version -OutputDirectory "$destinationPath"
nuget pack "$sourcePath\Antix.Html\Antix.Html.code.nuspec" -Properties version=$version -OutputDirectory "$destinationPath"
nuget pack "$sourcePath\Antix.Web\Antix.Web.code.nuspec" -Properties version=$version -OutputDirectory "$destinationPath"

if ($apiKey -ne $null) {
    
    # Push
    nuget SetApiKey $apiKey

    nuget push $destinationPath\Antix.code.$version.nupkg
    nuget push $destinationPath\Antix.Keywords.code.$version.nupkg
    nuget push $destinationPath\Antix.Keywords.EF.code.$version.nupkg
    nuget push $destinationPath\Antix.Drawing.code.$version.nupkg
    nuget push $destinationPath\Antix.Html.code.$version.nupkg
    nuget push $destinationPath\Antix.Web.code.$version.nupkg
}

read-host "Press enter to close..."