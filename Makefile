default:
	dotnet build

gen_pot:
	cd ./CrossPackageManager
	xgettext -k"_" -k"_n:1,2" -k"_p:1c,2" -k"_pn:1c,2,3" -o .\locale\CrsPkg.pot -c=TRANSLATORS: --from-code=UTF-8 Program.cs