// First sign in page.
import "../../styles/globals.css";
import React from "react";

export default function SignIn() {
    return (
        <div className="flex min-h-screen  flex-col items-center justify-center py-2">
            <form className="flex flex-col space-y-2">
                <label htmlFor="Email">
                    <span className="after:content-['*'] after:ml-0.5 after:text-red-500 block text-sm font-medium text-slate-700">
                        Email
                    </span>
                </label>
                <input
                    type="text"
                    className="border rounded border-slate-200 placeholder-slate-400 contrast-more:border-slate-400 contrast-more:placeholder-slate-500"
                    placeholder="Email"
                />

                <label htmlFor="password">
                    {" "}
                    <span className="after:content-['*'] after:ml-0.5 after:text-red-500 block text-sm font-medium text-slate-700">
                        Password
                    </span>
                </label>

                <input
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

                <a className="text-center cursor-pointer">
                    Already have account?
                </a>
            </form>
        </div>
    );
}
