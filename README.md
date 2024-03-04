# ForeignExchangeRates_Backend

I developed the API with the aim of demonstrating basic concepts and objectives on how to create clear and understandable code for everyone, ensuring test validation at each layer, whenever possible.

I chose to use the IHttpClientFactory for connections to the APIs, due to its greater resilience.

I implemented an error middleware, so we can log our requests, responses and errors in tools like Elasticsearch or New Relic.

I integrated the Azure Queue queue as part of the system, for its simplicity, but we could use a messaging mechanism like RabbitMQ or similar.

A consumer layer could be added to process queue messages efficiently.

To run:

To run locally, you must delete the files from the migrations folder in the Infra layer, don't forget to change the appsettings.json with the bank strings. If you want to run pointing to the current environment, you need to connect to the database once, as it goes into a shutdown state after 10 minutes of no connections.