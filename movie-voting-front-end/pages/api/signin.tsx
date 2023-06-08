// First sign in page.
import { useRouter } from "next/router";
import "../_app";
import React from "react";
import { Elsie_Swash_Caps } from "next/font/google";

export default function SignIn() {
    const router = useRouter();

    /**
     * Navigates to the sign-up page.
     *
     * This function does not return anything.
     */
    const navigateSignUp = () => {
        router.push("/sign-up");
    };

    /**
     * Handles the form submission for user login.
     * @param e - The form submission event.
     * @returns void
     */
    const handleSubmit = (e: React.FormEvent<HTMLFormElement>): void => {
        e.preventDefault();
        handleLogin(e);
    };

    async function handleLogin(e: React.FormEvent<HTMLFormElement>) {
        const formData = new FormData(e.currentTarget);
        const email = formData.get("email") as string;
        const password = formData.get("password") as string;

        var json = JSON.stringify({ email: email, password: password });
        var requestOptions = {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: json,
        };

        // log data
        // console.log(json);
        console.log(requestOptions);
        // log data

        const response = await fetch("/api/sign-in", requestOptions);
        if (response.ok) {
            const data = await response.json();
            localStorage.setItem("jwtToken", data.token);
            router.push("/dashboard");
        } else {
            console.log(response);
        }
    }

    return (
        <div className="flex min-h-screen  flex-col items-center justify-center py-2">
            <form className="flex flex-col space-y-2" onSubmit={handleSubmit}>
                <label htmlFor="email">
                    <span className="after:content-['*'] after:ml-0.5 after:text-red-500 block text-sm font-medium text-slate-700">
                        Email
                    </span>
                </label>
                <input
                    name="email"
                    type="text"
                    className="border rounded border-slate-200 placeholder-slate-400 contrast-more:border-slate-400 contrast-more:placeholder-slate-500"
                    placeholder="Email"
                />

                <label htmlFor="password">
                    <span className="after:content-['*'] after:ml-0.5 after:text-red-500 block text-sm font-medium text-slate-700">
                        Password
                    </span>
                </label>

                <input
                    name="password"
                    type="password"
                    className="border rounded border-slate-200 placeholder-slate-400 contrast-more:border-slate-400 contrast-more:placeholder-slate-500"
                    placeholder="Password"
                />

                <button
                    type="submit"
                    className="btn bg-indigo-500 rounded-md px-2 py-2 text-sm font-medium text-white hover:bg-indigo-600 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                >
                    Login
                </button>

                <a
                    className="text-center cursor-pointer"
                    onClick={() => {
                        navigateSignUp();
                    }}
                >
                    Don&apos;t have an account? Register here.
                </a>
            </form>
        </div>
    );
}
