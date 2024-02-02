
// The items the customer wants to buy
const items = [{ id: "xl-tshirt" }];
let sessionId;
let amount;
let domain;
let elements;
const urlPayment = document.getElementById("urlPayment").value;
const urlSuccess = document.getElementById("urlSuccess").value;
const urlFail = document.getElementById("urlFail").value;
const publicKey = document.getElementById("publicKey").value;
// This is your test publishable API key.
const stripe = Stripe(publicKey);


initialize();
//checkStatus();

document
    .querySelector("#payment-form")
    .addEventListener("submit", handleSubmit);

var emailAddress = '';
async function pay() {
    initialize();
}
// Fetches a payment intent and captures the client secret
async function initialize() {
     amount = new URLSearchParams(window.location.search).get(
        "amount"
    );
    sessionId = new URLSearchParams(window.location.search).get(
        "sessionId"
    );
    domain = new URLSearchParams(window.location.search).get(
        "domain"
    );
    const response = await fetch("api/stripe/create-payment-intent", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ amount }),
    });
    const { clientSecret } = await response.json();
    const locale = "ja";
    const appearance = {
        theme: 'stripe',
    };
    elements = stripe.elements({ appearance, clientSecret, locale });

    const linkAuthenticationElement = elements.create("linkAuthentication");
    linkAuthenticationElement.mount("#link-authentication-element");

    linkAuthenticationElement.on('change', (event) => {
        emailAddress = event.value.email;
    });

    const paymentElementOptions = {
        layout: "tabs",
    };

    const paymentElement = elements.create("payment", paymentElementOptions);
    paymentElement.mount("#payment-element");
}

async function handleSubmit(e) {
    e.preventDefault();
    setLoading(true);
    const url = domain + urlPayment
    const { error } = await stripe.confirmPayment({
        elements,
        confirmParams: {
            // Make sure to change this to your payment completion page
            return_url: url + "?total=" + amount + "&sessionId=" + sessionId + "",
            receipt_email: 'abc@localhost.com'//emailAddress,
        },
    });

    // This point will only be reached if there is an immediate error when
    // confirming the payment. Otherwise, your customer will be redirected to
    // your `return_url`. For some payment methods like iDEAL, your customer will
    // be redirected to an intermediate site first to authorize the payment, then
    // redirected to the `return_url`.
    if (error.type === "card_error" || error.type === "validation_error") {
        showMessage(error.message);
    } else {
        showMessage("An unexpected error occurred.");
    }

    setLoading(false);
}

// Fetches the payment intent status after payment submission
async function checkStatus() {
    const clientSecret = new URLSearchParams(window.location.search).get(
        "payment_intent_client_secret"
    );

    if (!clientSecret) {
        return;
    }

    const { paymentIntent } = await stripe.retrievePaymentIntent(clientSecret);

    switch (paymentIntent.status) {
        case "succeeded":
            showMessage("Payment succeeded!");
            break;
        case "processing":
            showMessage("Payment processing");
            break;
        case "requires_payment_method":
            showMessage("Payment requires_payment_method!");
            break;
        default:
            showMessage("Payment error");
            break;
    }
}

// ------- UI helpers -------

function showMessage(messageText) {
    const messageContainer = document.querySelector("#payment-message");

    messageContainer.classList.remove("hidden");
    messageContainer.textContent = messageText;

    setTimeout(function () {
        messageContainer.classList.add("hidden");
        messageText.textContent = "";
    }, 4000);
}

// Show a spinner on payment submission
function setLoading(isLoading) {
    if (isLoading) {
        // Disable the button and show a spinner
        document.querySelector("#submit").disabled = true;
        document.querySelector("#spinner").classList.remove("hidden");
        document.querySelector("#button-text").classList.add("hidden");
    } else {
        document.querySelector("#submit").disabled = false;
        document.querySelector("#spinner").classList.add("hidden");
        document.querySelector("#button-text").classList.remove("hidden");
    }
}