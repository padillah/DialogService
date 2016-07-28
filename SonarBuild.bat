path=%path%;C:\sonarqube-5.6.1\MSBuild.SonarQube.Runner-2.1

msbuild.sonarQube.runner begin /n:DialogService /k:DialogServiceKey /v:%1

msbuild

msbuild.sonarQube.runner end
