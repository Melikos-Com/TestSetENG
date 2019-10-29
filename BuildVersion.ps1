param(
[string]$assemblyVersion="0.0.0.0",
[string]$fileAssemblyVersion="0.0.0.0"
) 

function Update-SourceVersion
{
  Param
  (
    [string]$SrcPath,
    [string]$assemblyVersion, 
    [string]$fileAssemblyVersion
  )
     
    $buildNumber = $Env:BUILD_BUILDNUMBER
    if ($buildNumber -eq $null)
    {
        $buildIncrementalNumber = 0
    }
    else
    {
        $splitted = $buildNumber.Split('.')
        $buildIncrementalNumber = $splitted[$splitted.Length - 1]
    }
 
    if ($fileAssemblyVersion -eq "")
    {
        $fileAssemblyVersion = $assemblyVersion
    }
     
    Write-Host "Executing Update-SourceVersion in path $SrcPath, Version is $assemblyVersion and File Version is $fileAssemblyVersion"
  
 
    $AllVersionFiles = Get-ChildItem $SrcPath AssemblyInfo.cs -recurse
 
   Write-Host  "file:" $AllVersionFiles
     

     $Year = get-date -format yy
     $JulianYear = $Year.Substring(1)
     $DayOfYear = (Get-Date).DayofYear
     $jdate = $JulianYear + "{0:D3}" -f $DayOfYear


    
    $assemblyVersion = $assemblyVersion.Replace("J", $jdate).Replace("B", $buildIncrementalNumber)
    $fileAssemblyVersion = $fileAssemblyVersion.Replace("J", $jdate).Replace("B", $buildIncrementalNumber)
     
    Write-Host "Transformed Version is $assemblyVersion and Transformed File Version is $fileAssemblyVersion"
  
    
 
    foreach ($file in $AllVersionFiles) 
    { 
        Write-Host "Modifying file " + $file.FullName
        #save the file for restore
        $backFile = $file.FullName + "._ORI"
        $tempFile = $file.FullName + ".tmp"
        Copy-Item $file.FullName $backFile
        #now load all content of the original file and rewrite modified to the same file
        Get-Content $file.FullName |
        %{$_ -replace 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', "AssemblyVersion(""$assemblyVersion"")" } |
        %{$_ -replace 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', "AssemblyFileVersion(""$fileAssemblyVersion"")" }  > $tempFile
        Move-Item $tempFile $file.FullName -force
    }
  
}


Write-Host "Running Pre Build Scripts"
$scriptRoot = $PSCommandPath | Split-Path -Parent

Update-SourceVersion $scriptRoot $assemblyVersion $fileAssemblyVersion

