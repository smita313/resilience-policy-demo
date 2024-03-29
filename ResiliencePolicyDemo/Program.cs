using ResiliencePolicyDemo;

await Scenarios.Successful();

// await Scenarios.Retry_NeverSucceed();

// await Scenarios.Retry_IntermittentSucceed();

// await Scenarios.CircuitBreaker();

// await Scenarios.Timeout();