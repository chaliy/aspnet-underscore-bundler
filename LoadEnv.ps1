if (!($env:path -like "*nuget*")){
	$fullpath = (Resolve-Path .nuget)
	write-host "Add nuget [$fullpath]"
	$env:path = ($env:path + ";" + $fullpath)
}
gci .\packages -Recurse -Filter "tools" | %{	
	if (!($env:path -like "*$_*")){
		write-host Add $_
		$env:path = ($env:path + ";" + $_)
	}
}