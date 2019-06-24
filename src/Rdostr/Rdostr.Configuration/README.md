# Rdostr.Configuration

Demonstrates a configuration service that provides a Key (stored in Azure Key Vault) to
a User that is authenticated by a Bearer JWT (issued by AAD / B2C) and authorized by Claims.

> <https://docs.microsoft.com/en-us/aspnet/core/security/key-vault-configuration?view=aspnetcore-2.2#use-managed-identities-for-azure-resources>

```bash
# Create Resource Group
az group create -n "rdostr-rg" --location australiaeast

# Create Key Vault
az keyvault create -n "rdostr-aue-kv" --resource-group "rdostr-rg" --location australiaeast

# Create Secret
az keyvault secret set --vault-name "rdostr-aue-kv" --name "Rdostr-Mobile--DataKey1" --value "(secret key value)"

# Enable the API App Managed Identity
# e.g. if hosting in an App Service:
az webapp identity assign -g "rdostr-rg" -n "rdostr-config-aue"
# ...returns an Object Id for the next step...

# Grant the App Service permission to read the Key Vault
az keyvault set-policy --name "rdostr-aue-kv" --object-id $objectId --secret-permissions get list

# Restart the App
```
