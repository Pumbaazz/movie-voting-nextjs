export default function SignUp() {
    return (
        <div className="flex min-h-screen  flex-col items-center justify-center py-2">
            <form className="flex flex-col space-y-2">
                <label htmlFor="username">Username</label>
                <input
                    type="text"
                    className="border rounded border-slate-200 placeholder-slate-400 contrast-more:border-slate-400 contrast-more:placeholder-slate-500"
                    placeholder="Username"
                />
                <label htmlFor="password">Password</label>
                <input
                    type="password"
                    className="border rounded border-slate-200 placeholder-slate-400 contrast-more:border-slate-400 contrast-more:placeholder-slate-500"
                    placeholder="Password"
                />
                <button type="submit" className="btn bg-indigo-500">
                    Login
                </button>
            </form>
        </div>
    );
}
