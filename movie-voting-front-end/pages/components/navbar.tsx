import { useRouter } from "next/router";
import "../_app";
import React from "react";

/**
 * Renders a navigation bar with a logout button that removes the jwtToken from
 * localStorage and redirects to the homepage when clicked.
 *
 * @return {void}
 */
export default function Navbar() {
    const router = useRouter();

    /**
     * Removes the jwtToken from localStorage and redirects to homepage.
     *
     * @return {void}
     */
    const logout = () => {
        localStorage.removeItem("jwtToken");
        router.push("/");
    };

    return (
        <>
            <nav className="flex items-center justify-between p-8">
                <h1>Dashboard</h1>
                <button
                    className="btn border bg-green-400"
                    onClick={() => logout()}
                >
                    Log out
                </button>
            </nav>
        </>
    );
}
