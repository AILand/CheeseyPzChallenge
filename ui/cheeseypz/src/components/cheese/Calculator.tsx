import React, { useEffect } from 'react';

import { CheeseModel } from '../../models/cheese';
import { formHelper } from '../../util/form-helper';

import styles from './styles.module.scss';

interface IProps {
  cheeses: CheeseModel[];
}

const Calculator: React.FC<IProps> = props => {
  const [cheeseList, setCheeseList] = React.useState<CheeseModel[]>([]);
  const [totalPrice, setTotalPrice] = React.useState<number>(0);
  const [totalWeight, setTotalWeight] = React.useState<number>(0);
  const [pricePerKg, setPricePerKg] = React.useState<number>(0);

  const onChangeCheese = (e: any) => {
    const { value } = e.target;
    setPricePerKg(value);
  };

  const onChangeWeight = (e: any) => {
    const { value } = e.target;
    setTotalWeight(value);
  };

  useEffect(() => {
    setCheeseList(props.cheeses);
  }, [props.cheeses]);

  useEffect(() => {
    console.log('totalWeight: ', totalWeight, 'pricePerKg: ', pricePerKg);
    setTotalPrice(totalWeight * pricePerKg);
  }, [totalWeight, pricePerKg]);

  useEffect(() => {
    //rerender
  }, [totalPrice]);

  return (
    <div style={{ border: 'black 1px solid', borderRadius: '5px', padding: '10px' }}>
      <div className={styles.title}>Price Calculator</div>
      <div style={{ display: 'block' }}>
        <div style={{ display: 'inline' }}>
          Cheese:
          <div style={{ display: 'inline', paddingLeft: 28 }}>
            <select onChange={onChangeCheese} placeholder="Select a cheese">
              <option key={'blank'} value={0}></option>
              {cheeseList.map(cheese => (
                <option key={cheese.id} value={cheese.pricePerKg}>
                  {cheese.name}
                </option>
              ))}
            </select>
          </div>
        </div>
        <div>
          Kg:
          <div style={{ display: 'inline', paddingLeft: 60 }}>
            <input type="number" step={0.001} min={0} max={1000} name="weight" onChange={onChangeWeight} />
          </div>
        </div>
      </div>
      <div style={{ display: 'block' }}>
        Total:
        <div style={{ display: 'inline', paddingLeft: 45 }}>{formHelper.formatNumberToDollars(totalPrice, '$', '$0')}</div>
      </div>
    </div>
  );
};

export default Calculator;
