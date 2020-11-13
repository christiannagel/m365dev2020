# Azure App Configuration

> Code samples for Azure App Configuration for the Global Microsoft 365 Developer Bootcamp Austria 2020

## Create Azure App Configuration

```bash
rg=rg-m365
loc=westeurope
conf=m365config
key1=AppConfigurationSample:Settings:Config1
val1="configuration value for key 1"
devval1="development value for key 1"
stagingval1="staging value for key 1"
prodval1="production value for key 1"
sentinelKey=AppConfigurationSample:Settings:Sentinel
sentinelValue=1

# az group create --location $loc --name $rg
az appconfig create --location $loc --name $conf --resource-group $rg 
az appconfig kv set -n $conf --key $key1 --value "$val1" -y

az appconfig kv set -n $conf --key $key1 --label Development --value "$devval1" -y

az appconfig kv set -n $conf --key $key1 --label Staging --value "$stagingval1" -y

az appconfig kv set -n $conf --key $key1 --label Production --value "$prodval1" -y

az appconfig kv set -n $conf --key $sentinelKey --value $sentinelValue -y

```

## Feature Flags

```bash
az appconfig feature set --feature FeatureX -n $conf
```
