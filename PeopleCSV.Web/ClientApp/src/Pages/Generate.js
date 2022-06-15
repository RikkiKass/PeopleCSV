import React, { useState } from "react";


const Generate = () => {

    const [amount, setAmount] = useState(0);

    const onGenerateClick = () => {
        window.location.href = `/api/peoplecsv/GenerateCsv/${amount}`;
    }
    return (
        <div className="container">
            <input type='text' placeholder="Enter an amount" className="form-control" onChange={e => setAmount(e.target.value)}></input>
            <button className="btn btn-primary" onClick={onGenerateClick}>
                Generate
            </button>
        </div>
    )
}
export default Generate;