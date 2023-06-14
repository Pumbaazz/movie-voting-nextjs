import { useRouter } from "next/router";
import "./_app";
import React from "react";
import Swal from "sweetalert2";

export default function SignUp() {
    const router = useRouter();

    const handleFormSubmit = async (
        event: React.FormEvent<HTMLFormElement>
    ): Promise<void> => {
        event.preventDefault();
        const formData = new FormData(event.currentTarget);
        const name = formData.get("name") as string;
        const email = formData.get("email") as string;
        const password = formData.get("password") as string;

        const requestOptions: RequestInit = {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ name, email, password }),
        };

        let statusText = "";

        try {
            const response = await fetch("/api/sign-up", requestOptions);
            if (response.ok) {
                statusText = "User created successfully.";
            } else {
                switch (response.status) {
                    case 409:
                        statusText = "User already exists. Please try again.";
                        break;
                    case 401:
                        statusText = "Invalid input. Please try again.";
                        break;
                    default:
                        statusText =
                            "An error occurred. Please try again later.";
                        break;
                }
            }
        } catch (error: any) {
            // console.error(`Error: ${error}`);
            statusText = error.message;
        } finally {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: `${statusText}`,
            });
        }
    };

    const goToSignIn = () => {
        router.push("/");
    };

    return (
        <div className="flex min-h-screen flex-col items-center justify-center py-2">
            <form
                className="flex flex-col space-y-2"
                onSubmit={handleFormSubmit}
            >
                <label htmlFor="name">
                    <span className="after:content-['*'] after:ml-0.5 after:text-red-500 block text-sm font-medium text-slate-700">
                        Name
                    </span>
                </label>
                <input
                    name="name"
                    type="text"
                    className="border rounded border-slate-200 placeholder-slate-400 contrast-more:border-slate-400 contrast-more:placeholder-slate-500"
                    placeholder="Name"
                />

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
                    Sign Up
                </button>

                <a className="text-center cursor-pointer" onClick={goToSignIn}>
                    Already have an account? Login here.
                </a>
            </form>
        </div>
    );
}
