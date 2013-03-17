param($installPath, $toolsPath, $package, $project)

$project.Save()

# Find .settings files without generator set, sets and associates with designer file
$xml = [XML] (Get-Content $project.FullName)
$xmlns = $xml.Project.GetAttribute("xmlns")

$nsmgr = New-Object System.Xml.XmlNamespaceManager -ArgumentList $xml.NameTable
$nsmgr.AddNamespace('ns',$xmlns)

$nodes = $xml.Project.SelectNodes("//ns:*[@Include[substring(., string-length()-8)='.settings']]",$nsmgr)
$nodes | ForEach-Object { 
    if( $_.Generator -eq $null ) {
        Write-Host $_

        $newNode = $xml.CreateElement("Generator", $xmlns)
        $newNode.InnerXml = "PublicSettingsSingleFileGenerator"
        $_.AppendChild($newNode)

        $include = $_.Attributes["Include"].Value;
        $lastSlashIndex = $include.LastIndexOf('\')+1
        $name = $include.Substring($lastSlashIndex, $include.Length -$lastSlashIndex - 9)
        $depName = "$name.Designer.cs"

        $newNode = $xml.CreateElement("LastGenOutput", $xmlns)
        $newNode.InnerXml = $depName
        $_.AppendChild($newNode)

        $depNameLength = $depName.Length - 1;
        $xpath = "//ns:Compile[@Include[substring(., string-length()-$depNameLength)='$depName']]"
        $dep = $xml.Project.SelectSingleNode($xpath, $nsmgr)

        $newNode = $xml.CreateElement("AutoGen", $xmlns)
        $newNode.InnerXml = "True"
        $dep.AppendChild($newNode)

        $newNode = $xml.CreateElement("DesignTimeSharedInput", $xmlns)
        $newNode.InnerXml = "True"
        $dep.AppendChild($newNode)

        $newNode = $xml.CreateElement("DependentUpon", $xmlns)
        $newNode.InnerXml = "$name.settings"
        $dep.AppendChild($newNode)
    }
}

$xml.Save($project.FullName)