const API_BASE = "https://eternocelllottery.giokiplanet.com";

const form = document.getElementById("lotteryForm");
form.addEventListener("submit", function (event) {
    event.preventDefault();
    createUser();
});



async function createUser() {
    const firstName = document.getElementById("firstName").value.trim();
    const lastName = document.getElementById("lastName").value.trim();
    const phoneNumber = document.getElementById("phoneNumber").value.trim();
    const msg = document.getElementById("msg");
    const submitBtn = document.getElementById("submitBtn");

    msg.innerText = "";
    msg.className = "";

    if (!firstName || !lastName) {
        msg.innerText = "نام و نام خانوادگی الزامی است.";
        msg.className = "error";
        return;
    }

    if (phoneNumber && !/(\+98|0|0098)9\d{9}$/.test(phoneNumber)) {
        msg.innerText = "شماره موبایل صحیح نیست.";
        msg.className = "error";
        return;
    }

    submitBtn.disabled = true;

    const response = await fetch(`${API_BASE}/api/User/CreateUser`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ firstName, lastName, phoneNumber })
    });

    if (response.ok) {
        msg.innerText = "اطلاعات شما با موفقیت ثبت شد 🎉";
        msg.className = "success";
        document.getElementById("firstName").value = "";
        document.getElementById("lastName").value = "";
        document.getElementById("phoneNumber").value = "";
        submitBtn.disabled = true;
        form.classList.add("hidden");
        await getUserCount();
    } else {
        msg.innerText = "در ثبت اطلاعات خطایی رخ داد.";
        msg.className = "error";
        submitBtn.disabled = false;
    }
}

async function getUserCount() {
    const response = await fetch(`${API_BASE}/api/User/GetAllUserCount`);
    if (response.ok) {
        const count = await response.json();
        document.getElementById("userCount").innerText = "تعداد شرکت‌کنندگان: " + count;
    } else {
        document.getElementById("userCount").innerText = "تعداد شرکت‌کنندگان: خطا";
    }
}

// Load count at startup
getUserCount();