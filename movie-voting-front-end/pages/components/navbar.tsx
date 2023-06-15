import { useRouter } from "next/router";
import "../_app";
import React, { useEffect, useState } from "react";
import jwtDecode from "jwt-decode";
import { User } from "@/interfaces";

export default function Navbar() {
    const router = useRouter();
    const [currentUser, setCurrentUser] = useState<User | null>(null);

    useEffect(() => {
        const token = localStorage.getItem("jwtToken");
        if (token) {
            let decoded: User = jwtDecode(token) as {
                id: string;
                name: string;
                email: string;
            };
            setCurrentUser(decoded);
        }
    }, []);

    const logout = () => {
        localStorage.removeItem("jwtToken");
        router.push("/");
    };

    return (
        <>
            <nav className="flex items-center justify-between p-8">
                <h1 className="text-2xl font-medium">
                    Welcome{" "}
                    <a href="#">{currentUser?.name ?? "Unknown User"}</a>
                </h1>
                <button
                    className="btn border bg-green-400 rounded-full px-2 py-2 text-sm font-medium text-white hover:bg-green-500 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500 drop-shadow-md"
                    onClick={() => logout()}
                >
                    Log out
                </button>
            </nav>
        </>
    );
}
