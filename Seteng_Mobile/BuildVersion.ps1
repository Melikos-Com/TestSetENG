param(
    [string]$assemblyVersion = "0.0.0"
) 

function Update-SourceVersion {
    Param
    (
        [string]$SrcPath,
        [string]$assemblyVersion
    
    )
     
    $buildNumber = $Env:BUILD_BUILDNUMBER

    if ($buildNumber -eq $null) {
        $buildIncrementalNumber = 0
    }
    else {
        $splitted = $buildNumber.Split('.')
        $buildIncrementalNumber = $splitted[$splitted.Length - 1]
    }
 
 
    $AllVersionFiles = Get-ChildItem $SrcPath config.xml -recurse
 
    Write-Host  "file:" $AllVersionFiles
     

    $Year = get-date -format yy
    $JulianYear = $Year.Substring(1)
    $DayOfYear = (Get-Date).DayofYear
    $jdate = $JulianYear + "{0:D3}" -f $DayOfYear
    
    
    $assemblyVersion = $assemblyVersion.Replace("J", $jdate).Replace("B", $buildIncrementalNumber)
   
     
    Write-Host "Transformed Version is $assemblyVersion"
  
    
 
    foreach ($file in $AllVersionFiles) { 
        Write-Host "Modifying file " + $file.FullName
        #save the file for restore
        $backFile = $file.FullName + "._ORI"
        $tempFile = $file.FullName + ".tmp"
        Copy-Item $file.FullName $backFile
        #now load all content of the original file and rewrite modified to the same file
        Get-Content $file.FullName -Encoding UTF8 |
            %  {$_ -replace 'version="[0-9]+(\.([0-9]+|\*)){2,3}"', "version=""$assemblyVersion""" } | Out-File $tempFile -Encoding UTF8  
        Move-Item $tempFile $file.FullName -force
    }
  
}


Write-Host "Running Pre Build Scripts"
$scriptRoot = $PSCommandPath | Split-Path -Parent

Update-SourceVersion $scriptRoot $assemblyVersion 

