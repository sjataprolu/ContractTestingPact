# ContractTestingPact
#Steps
1. Create account in pactflow
2. Install docker
3. download docker image from https://hub.docker.com/r/pactfoundation/pact-cli
4. to download certificate, login to pactflow in firefox and download cert
5. run the following command to publish pact
6. docker run --rm -w /pacts -v C:\Users\Srinivasa.Jataprolu1\source\repos\CollinsonConsumerTests\pacts\srinijataprolu-pactflow-io-chain.pem:/pacts/srinijataprolu-pactflow-io-chain.pem -e SSL_CERT_FILE=/pacts/srinijataprolu-pactflow-io-chain.pem -v C:\Users\Srinivasa.Jataprolu1\source\repos\CollinsonConsumerTests\pacts\Consumer-Provider.json:/pacts/Consumer-Provider.json -e PACT_BROKER_BASE_URL="https://srinijataprolu.pactflow.io/" -e PACT_BROKER_USERNAME="srinivasa.jataprolu1@collinsongroup.com" -e PACT_BROKER_TOKEN="ab8YMCsYyivnD6e6R-atqw" pactfoundation/pact-cli:latest publish /pacts/Consumer-Provider.json --consumer-app-version version1-PolicyIssued
