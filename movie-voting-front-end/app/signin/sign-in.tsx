// First sign in page.

import Router from "next/dist/server/router";

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
                    className="placeholder:italic placeholder:text-slate-400 block bg-white w-full border border-slate-300 rounded-md py-2 pl-9 pr-3 shadow-sm focus:outline-none focus:border-sky-500 focus:ring-sky-500 focus:ring-1 sm:text-sm"
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
                    className="placeholder:italic placeholder:text-slate-400 block bg-white w-full border border-slate-300 rounded-md py-2 pl-9 pr-3 shadow-sm focus:outline-none focus:border-sky-500 focus:ring-sky-500 focus:ring-1 sm:text-sm"
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
