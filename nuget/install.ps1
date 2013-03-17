param($installPath, $toolsPath, $package, $project)

$project.Save()

function UpdateProjectItem{
    param($projectItem, [string]$namespace)

    if ($projectItem.Kind -eq "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}") {
        $newNamespace = $namespace + "." + $projectItem.Name 

		UpdateProjectItems $projectItem.ProjectItems $newNamespace
	}

    elseif( $projectItem.Name.EndsWith(".settings") ) {

        $property = $projectItem.Properties.Item("CustomTool");

        if($property.Value -eq "") {
            Write-Host "Running custom tool on"$projectItem.Name 
            $property.Value = "PublicSettingsSingleFileGenerator"
            $property = $projectItem.Properties.Item("CustomToolNamespace");
            $property.Value = $namespace.TrimStart(".")

            $projectItem.Object.RunCustomTool()
        }
    }
}

function UpdateProjectItems(  ) {
    param($projectItems, [string]$namespace)
	$projectItems | ForEach-Object { UpdateProjectItem $_ $namespace }
}

UpdateProjectItems $project.ProjectItems ""

$project.Save()