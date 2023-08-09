import React, { Children, useEffect, useState } from 'react';

const SearchResultCard = ({ title, date, parshah, volume, issue, displayText, language, parshahEnglish, year }) => {
    const itemName = `${title} (${parshahEnglish} ${year})`;
    const itemNumber = `${language} ${volume}${issue}`;
    const submitTitle = `Add Pamphlet to Cart - ${language}`;
    const viewLink = `https://ravmosheshapiro.com/parshah${volume}.html`

    if (title === undefined) {
        return (
            <>
                <div className='card z-depth-5 shadow my-2 mx-1 order-4 card-body text-center p-1 col-md-3 flex' style={{ height: 170, minWidth: 220 }}>
                </div>
            </>
        );
    }
    else {
        return (
            <div className='card z-depth-5 shadow my-2 mx-1 order-4 card-body text-center p-1 col-md-3 flex' style={{ height: 170, minWidth: 415 , maxWidth: 415}}>
                <h6 style={{ marginBottom: 0, marginTop: 8, fontSize: 17 }} >{title}</h6>
                <h6 style={{ margin: 0, fontSize: 9 }}>{parshah}</h6>
                <h6 style={{ margin: 0, fontSize: 8 }}>{date}</h6>
                <p className='lead' style={{ fontSize: 15, margin: 5 }}>...{displayText}...</p>
                <div className='row' >
                    <div className='col-md-2' style={{ maxWidth: 80 }}>
                        <p className='small' style={{ fontSize: 8, marginTop: 8, marginBottom: 0 }}>Volume-{volume}, Issue-{issue}</p>
                    </div>
                    <div className='col-md-3 offset-4' style={{maxWidth: 100} }>
                        <a href={viewLink} className='btn btn-outline-dark w-100' style={{ fontSize: 12, padding: 2, color: 'navy', outlineColor: 'navy' }}>View</a>
                    </div>

                    <div className='col-md-3' style={{ maxWidth: 100 }}>
                        <form target="paypal" action="https://www.paypal.com/cgi-bin/webscr" method="post" className="form-inline p-0 mx-auto my-0">
                            <input type="hidden" name="cmd" value="_cart" />
                            <input type="hidden" name="add" value="1" />
                            <input type="hidden" name="bn" value="webassist.dreamweaver.4_0_1" />
                            <input type="hidden" name="business" value="zyapublications@gmail.com" />
                            <input type="hidden" name="item_name" value={itemName} />
                            <input type="hidden" name="item_number" value={itemNumber} />
                            <input type="hidden" name="amount" value="3.95" />
                            <input type="hidden" name="currency_code" value="USD" />
                            <input type="hidden" name="receiver_email" value="zyapublications@gmail.com" />
                            <input type="hidden" name="mrb" value="R-3WH47588B4505740X" />
                            <input type="hidden" name="pal" value="ANNSXSLJLYR2A" />
                            <input type="hidden" name="no_shipping" value="0" />
                            <input type="hidden" name="no_note" value="0" />
                            <input type="hidden" name="weight" value=".13" />
                            <input type="hidden" name="weight_unit" value="lbs" />
                            <input type="submit" value="Purchase" name="submit" title={submitTitle} className='btn btn-outline-dark w-100' style={{ fontSize: 12, padding: 2, color: 'navy', outlineColor: 'navy' }} />

                        </form>
                    </div>
                </div>
            </div >
        );
    }
}

/*<input type="submit" value="Buy" name="submit" title={submitTitle} className="atcButton btn btn-dark px-2 py-0 mx-auto m-0 d-sm-none" />*/
export default SearchResultCard;

