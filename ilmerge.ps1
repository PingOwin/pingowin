	$base_directory = Resolve-Path .
	$src_directory = "$base_directory\source"
	$output_directory = "$base_directory\build"
	$dist_directory = "$base_directory\distribution"

	$ilmerge_path = "$src_directory\packages\ILMerge.2.14.1208\tools\ILMerge.exe"
	$nuget_path = "$src_directory\.nuget\nuget.exe"

	$buildNumber = 0;
	$version = "2.5.0.0"
	$preRelease = $null


$input_dlls = "$output_directory\PingOwin.Core.Frontend.dll"

Get-ChildItem -Path $output_directory -Filter *.dll |
    foreach-object {
        # Exclude IdentityServer3.dll as that will be the primary assembly
        if ("$_" -ne "PingOwin.Core.Frontend.dll" -and "$_" -ne "Owin.dll" -and "$_" -ne "PingOwin.Core.dll") {
            $input_dlls = "$input_dlls $output_directory\$_"
        }
}

New-Item $dist_directory\lib\net45 -Type Directory
Invoke-Expression "$ilmerge_path /targetplatform:v4 /internalize /allowDup /target:library /out:$dist_directory\lib\net45\PingOwin.dll $input_dlls"
