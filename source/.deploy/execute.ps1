Param($path,$version)

$apikey = "API-NLITTOD95WSOKIT7FLWVSKTGIW"
$octopath = "http://deploy.antix.local"

if ($version -Eq $null) {
    $version = Read-Host "Enter Version Number"
}

if($path -Eq $null){
    $path = Split-Path -parent $PSCommandPath 
    $path = "$path\..\.."

    set-alias msbuild "C:\Program Files (x86)\MSBuild\12.0\Bin\amd64\MSBuild.exe"

    msbuild "$path\source\antix.sln" /t:Build /p:Configuration=Release /p:RunOctoPack=true /p:OctoPackPackageVersion=$version /p:OctoPackEnforceAddingFiles=true
}

Write-Output "begin deploy version $version from $path"

set-alias nuget $path\source\.nuget\NuGet.exe
set-alias octo $path\source\.deploy\Octo.exe

function deploy{
	param($project)

	$packagePath = "$path\source\$project\obj"
	$package = "$packagePath\$project.code.$version.nupkg"

	nuget pack "$path\source\$project\$project.code.nuspec" -Properties version=$version -OutputDirectory "$packagePath"
	
	write-Output "pushing $package"

	nuget push "$package" -ApiKey $apikey -Source $octopath/nuget/packages
}

deploy "Antix"
deploy "Antix.Data.Keywords"
deploy "Antix.Data.Keywords.EF"
deploy "Antix.Data.Static"
deploy "Antix.Drawing"
deploy "Antix.Html"
deploy "Antix.Http"
deploy "Antix.Services"
deploy "Antix.Services.ActionCache"
deploy "Antix.Security"
deploy "Antix.Web"
