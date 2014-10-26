Param($path,$version)

$apikey = "API-NLITTOD95WSOKIT7FLWVSKTGIW"
#$packagesSource = "http://localhost:50358/"
$packagesSource = "http://nuget.antix.co.uk/packages"
#$packagesSource = "http://deploy.antix.local/nuget/packages"

if ($version -Eq $null) {
    $version = Read-Host "Enter Version Number"
}

if($path -Eq $null){
    $path = Split-Path -parent $PSCommandPath 
    $path = "$path\..\.."

    set-alias msbuild "C:\Program Files (x86)\MSBuild\12.0\Bin\amd64\MSBuild.exe"

    msbuild "$path\source\antix.sln" /t:Build /p:Configuration=Release
}

Write-Output "begin deploy version $version from $path"

set-alias nuget $path\source\.nuget\NuGet.exe

function deploy{
	param($project)

	$packagePath = "$path\source\$project\obj"
	$package = "$packagePath\$project.code.$version.nupkg"

	nuget pack "$path\source\$project\$project.code.nuspec" -Properties version=$version -OutputDirectory "$packagePath"
	
	write-Output "pushing $package"

	nuget push "$package" -ApiKey $apikey -Source "$packagesSource/"
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

