const API = "http://localhost:5000/api";

function getToken() {
    return localStorage.getItem("token");
}

function getHeaders() {
    return {
        "Content-Type": "application/json",
        "Authorization": "Bearer " + getToken()
    };
}

async function register() {
    const username = document.getElementById("registerUsername").value;
    const password = document.getElementById("registerPassword").value;

    const response = await fetch(API + "/auth/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password })
    });

    const data = await response.json();
    document.getElementById("message").textContent =
        data.message || "Registrering klar.";
}

async function login() {
    const username = document.getElementById("loginUsername").value;
    const password = document.getElementById("loginPassword").value;

    const response = await fetch(API + "/auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password })
    });

    const data = await response.json();

    if (!response.ok) {
        document.getElementById("message").textContent = data.message;
        return;
    }

    localStorage.setItem("token", data.token);
    localStorage.setItem("role", data.role);

    document.getElementById("auth").style.display = "none";
    document.getElementById("loggedIn").style.display = "block";

    if (data.role === "Admin")
        document.getElementById("admin").style.display = "block";

    getProducts();
}

function logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("role");
    location.reload();
}

async function getProducts() {
    const search = document.getElementById("search").value;

    const response = await fetch(
        API + "/products?search=" + encodeURIComponent(search)
    );

    const products = await response.json();
    const container = document.getElementById("products");
    container.innerHTML = "";

    products.forEach(product => {
        const div = document.createElement("div");
        div.className = "product";

        const name = document.createElement("h3");
        name.textContent = product.name;

        const price = document.createElement("p");
        price.textContent = "Pris: " + product.price + " kr";

        const description = document.createElement("p");
        description.textContent = product.description;

        const button = document.createElement("button");
        button.textContent = "Köp";
        button.onclick = () => createOrder(product.id);

        div.append(name, price, description, button);
        container.appendChild(div);
    });
}

async function createOrder(productId) {
    const response = await fetch(API + "/orders", {
        method: "POST",
        headers: getHeaders(),
        body: JSON.stringify({ productId })
    });

    const data = await response.json();

    alert(response.ok ? "Order skapad!" : data.message);
}

async function getMyOrders() {
    const response = await fetch(API + "/orders/my", {
        headers: getHeaders()
    });

    const orders = await response.json();
    const container = document.getElementById("orders");
    container.innerHTML = "";

    orders.forEach(order => {
        const div = document.createElement("div");
        div.className = "order";
        div.textContent =
            "Order #" + order.id + " - " +
            order.productName + " - " +
            order.price + " kr";

        container.appendChild(div);
    });
}

async function createProduct() {
    const name = document.getElementById("productName").value;
    const price = Number(document.getElementById("productPrice").value);
    const description = document.getElementById("productDescription").value;

    const response = await fetch(API + "/products", {
        method: "POST",
        headers: getHeaders(),
        body: JSON.stringify({ name, price, description })
    });

    alert(response.ok ? "Produkten skapades." : "Kunde inte skapa produkten.");

    if (response.ok)
        getProducts();
}

async function getAllOrders() {
    const response = await fetch(API + "/orders/all", {
        headers: getHeaders()
    });

    const orders = await response.json();
    const container = document.getElementById("allOrders");
    container.innerHTML = "";

    orders.forEach(order => {
        const div = document.createElement("div");
        div.className = "order";
        div.textContent =
            "Order #" + order.id +
            " | Kund: " + order.username +
            " | Produkt: " + order.productName +
            " | Pris: " + order.price + " kr";

        container.appendChild(div);
    });
}
